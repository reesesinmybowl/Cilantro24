import { NextResponse } from 'next/server';
import crypto from 'node:crypto';
import { getStravaAuthUrl } from '@/lib/strava';

export async function GET() {
  try {
    const state = crypto.randomBytes(24).toString('hex');
    const authUrl = getStravaAuthUrl(state);

    const response = NextResponse.redirect(authUrl);
    response.cookies.set('strava_oauth_state', state, {
      httpOnly: true,
      secure: process.env.NODE_ENV === 'production',
      sameSite: 'lax',
      path: '/',
      maxAge: 60 * 10,
    });

    return response;
  } catch (error) {
    console.error(error);
    return NextResponse.redirect(new URL('/?error=unable-to-start-login', process.env.STRAVA_REDIRECT_URI));
  }
}
