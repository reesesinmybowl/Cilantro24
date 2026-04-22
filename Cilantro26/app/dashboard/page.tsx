import ActivitiesList from '@/components/activities-list';

export default function DashboardPage() {
  return (
    <main className="container" style={{ maxWidth: 860 }}>
      <header style={{ margin: '1rem 0 2rem' }}>
        <p className="muted" style={{ marginBottom: '0.6rem' }}>
          Step 2 of 4
        </p>
        <h1 style={{ margin: 0 }}>Choose an activity</h1>
        <p className="muted" style={{ marginTop: '0.7rem' }}>
          Pick one of your recent Strava activities to preview your future typography design.
        </p>
      </header>

      <ActivitiesList />
    </main>
  );
}
