import { NextRequest, NextResponse } from 'next/server';
import { getValidTokens, fetchRecentActivities } from '@/lib/strava';

export async function GET(request: NextRequest) {
  const sessionId = request.cookies.get('strava_session')?.value;

  if (!sessionId) {
    return NextResponse.json({ error: 'Not signed in.' }, { status: 401 });
  }

  try {
    const tokens = await getValidTokens(sessionId);
    if (!tokens) {
      return NextResponse.json({ error: 'Session expired. Please connect with Strava again.' }, { status: 401 });
    }

    const activities = await fetchRecentActivities(tokens.accessToken);
    return NextResponse.json({ athleteName: tokens.athleteName, activities });
  } catch (error) {
    const message = error instanceof Error ? error.message : 'Failed to load activities.';
    return NextResponse.json({ error: message }, { status: 500 });
  }
}
