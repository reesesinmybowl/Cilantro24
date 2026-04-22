import crypto from 'node:crypto';

export type StravaTokenRecord = {
  accessToken: string;
  refreshToken: string;
  expiresAt: number;
  athleteId: number;
  athleteName: string;
};

const sessions = new Map<string, StravaTokenRecord>();

export function createSessionId() {
  return crypto.randomUUID();
}

export function saveSession(sessionId: string, data: StravaTokenRecord) {
  sessions.set(sessionId, data);
}

export function getSession(sessionId: string) {
  return sessions.get(sessionId);
}

export function clearSession(sessionId: string) {
  sessions.delete(sessionId);
}
