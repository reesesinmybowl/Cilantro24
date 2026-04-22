import { getSession, saveSession, type StravaTokenRecord } from '@/lib/session-store';

const STRAVA_OAUTH_BASE = 'https://www.strava.com/oauth';
const STRAVA_API_BASE = 'https://www.strava.com/api/v3';

type TokenExchangeResponse = {
  token_type: string;
  access_token: string;
  refresh_token: string;
  expires_at: number;
  athlete: {
    id: number;
    firstname: string;
    lastname: string;
  };
};

export type StravaActivity = {
  id: number;
  name: string;
  distance: number;
  moving_time: number;
  elapsed_time: number;
  total_elevation_gain: number;
  start_date_local: string;
  sport_type: string;
};

function env(name: string): string {
  const value = process.env[name];
  if (!value) {
    throw new Error(`Missing environment variable: ${name}`);
  }
  return value;
}

export function getStravaAuthUrl(state: string): string {
  const params = new URLSearchParams({
    client_id: env('STRAVA_CLIENT_ID'),
    response_type: 'code',
    redirect_uri: env('STRAVA_REDIRECT_URI'),
    approval_prompt: 'auto',
    scope: process.env.STRAVA_SCOPES ?? 'read,activity:read_all',
    state,
  });

  return `${STRAVA_OAUTH_BASE}/authorize?${params.toString()}`;
}

export async function exchangeCodeForTokens(code: string): Promise<StravaTokenRecord> {
  const res = await fetch(`${STRAVA_OAUTH_BASE}/token`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    body: new URLSearchParams({
      client_id: env('STRAVA_CLIENT_ID'),
      client_secret: env('STRAVA_CLIENT_SECRET'),
      code,
      grant_type: 'authorization_code',
    }).toString(),
    cache: 'no-store',
  });

  if (!res.ok) {
    const details = await res.text();
    throw new Error(`Token exchange failed (${res.status}): ${details}`);
  }

  const data = (await res.json()) as TokenExchangeResponse;
  return {
    accessToken: data.access_token,
    refreshToken: data.refresh_token,
    expiresAt: data.expires_at,
    athleteId: data.athlete.id,
    athleteName: `${data.athlete.firstname} ${data.athlete.lastname}`.trim(),
  };
}

export async function refreshAccessToken(refreshToken: string): Promise<StravaTokenRecord> {
  const res = await fetch(`${STRAVA_OAUTH_BASE}/token`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    body: new URLSearchParams({
      client_id: env('STRAVA_CLIENT_ID'),
      client_secret: env('STRAVA_CLIENT_SECRET'),
      grant_type: 'refresh_token',
      refresh_token: refreshToken,
    }).toString(),
    cache: 'no-store',
  });

  if (!res.ok) {
    const details = await res.text();
    throw new Error(`Token refresh failed (${res.status}): ${details}`);
  }

  const data = (await res.json()) as TokenExchangeResponse;
  return {
    accessToken: data.access_token,
    refreshToken: data.refresh_token,
    expiresAt: data.expires_at,
    athleteId: data.athlete.id,
    athleteName: `${data.athlete.firstname} ${data.athlete.lastname}`.trim(),
  };
}

export async function getValidTokens(sessionId: string): Promise<StravaTokenRecord | null> {
  const current = getSession(sessionId);
  if (!current) return null;

  const now = Math.floor(Date.now() / 1000);
  if (current.expiresAt > now + 60) {
    return current;
  }

  const refreshed = await refreshAccessToken(current.refreshToken);
  saveSession(sessionId, refreshed);
  return refreshed;
}

export async function fetchRecentActivities(accessToken: string): Promise<StravaActivity[]> {
  const url = new URL(`${STRAVA_API_BASE}/athlete/activities`);
  url.searchParams.set('per_page', '20');
  url.searchParams.set('page', '1');

  const res = await fetch(url.toString(), {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
    cache: 'no-store',
  });

  if (!res.ok) {
    const details = await res.text();
    throw new Error(`Unable to fetch activities (${res.status}): ${details}`);
  }

  const data = (await res.json()) as StravaActivity[];
  return data;
}
