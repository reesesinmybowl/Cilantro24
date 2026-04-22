'use client';

import { useEffect, useState } from 'react';
import type { StravaActivity } from '@/lib/strava';

type ActivitiesResponse = {
  athleteName: string;
  activities: StravaActivity[];
};

function metersToMiles(meters: number): string {
  return (meters * 0.000621371).toFixed(2);
}

function secondsToMinutes(seconds: number): string {
  return `${Math.round(seconds / 60)} min`;
}

export default function ActivitiesList() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [data, setData] = useState<ActivitiesResponse | null>(null);

  useEffect(() => {
    async function loadActivities() {
      setLoading(true);
      setError(null);
      try {
        const res = await fetch('/api/strava/activities', { cache: 'no-store' });
        const payload = (await res.json()) as ActivitiesResponse & { error?: string };

        if (!res.ok) {
          throw new Error(payload.error ?? 'Unable to load activities.');
        }

        setData(payload);
      } catch (err) {
        const message = err instanceof Error ? err.message : 'Unable to load activities.';
        setError(message);
      } finally {
        setLoading(false);
      }
    }

    loadActivities();
  }, []);

  if (loading) {
    return <p className="muted">Loading your recent activities…</p>;
  }

  if (error) {
    return (
      <div className="card">
        <p style={{ marginTop: 0, fontWeight: 600 }}>We couldn’t load your activities.</p>
        <p className="muted" style={{ marginBottom: 0 }}>
          {error}
        </p>
      </div>
    );
  }

  if (!data || data.activities.length === 0) {
    return (
      <div className="card">
        <p style={{ marginTop: 0, fontWeight: 600 }}>No recent activities found yet.</p>
        <p className="muted" style={{ marginBottom: 0 }}>
          Complete a ride, run, or walk in Strava and come back here to choose your activity.
        </p>
      </div>
    );
  }

  return (
    <div style={{ display: 'grid', gap: '0.85rem' }}>
      <p className="muted" style={{ margin: 0 }}>
        Welcome back, {data.athleteName || 'there'}.
      </p>
      {data.activities.map((activity) => (
        <article className="card" key={activity.id}>
          <div style={{ display: 'flex', justifyContent: 'space-between', gap: '0.8rem', flexWrap: 'wrap' }}>
            <h3 style={{ margin: 0 }}>{activity.name}</h3>
            <span className="muted">{new Date(activity.start_date_local).toLocaleDateString()}</span>
          </div>
          <p className="muted" style={{ marginBottom: 0 }}>
            {activity.sport_type} · {metersToMiles(activity.distance)} mi · {secondsToMinutes(activity.moving_time)}
          </p>
        </article>
      ))}
    </div>
  );
}
