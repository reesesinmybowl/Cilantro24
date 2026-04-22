import { NextRequest, NextResponse } from 'next/server';
import { createSessionId, saveSession } from '@/lib/session-store';
import { exchangeCodeForTokens } from '@/lib/strava';

export async function GET(request: NextRequest) {
  const url = new URL(request.url);
  const code = url.searchParams.get('code');
  const state = url.searchParams.get('state');
  const stateCookie = request.cookies.get('strava_oauth_state')?.value;
  const authError = url.searchParams.get('error');

  if (authError) {
    return NextResponse.redirect(new URL('/?error=authorization-denied', request.url));
  }

  if (!code || !state || !stateCookie || state !== stateCookie) {
    return NextResponse.redirect(new URL('/?error=invalid-oauth-state', request.url));
  }

  try {
    const tokens = await exchangeCodeForTokens(code);
    const sessionId = createSessionId();
    saveSession(sessionId, tokens);

    const response = NextResponse.redirect(new URL('/dashboard', request.url));
    response.cookies.set('strava_session', sessionId, {
      httpOnly: true,
      secure: process.env.NODE_ENV === 'production',
      sameSite: 'lax',
      path: '/',
      maxAge: 60 * 60 * 24 * 14,
    });
    response.cookies.set('strava_oauth_state', '', { path: '/', maxAge: 0 });

    return response;
  } catch (error) {
    console.error(error);
    return NextResponse.redirect(new URL('/?error=token-exchange-failed', request.url));
  }
}
