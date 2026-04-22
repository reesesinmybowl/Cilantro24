type HomePageProps = {
  searchParams: Promise<{ error?: string }>;
};

const errorMessages: Record<string, string> = {
  'unable-to-start-login': 'We could not start Strava login. Please try again.',
  'authorization-denied': 'You canceled Strava login. You can try again anytime.',
  'invalid-oauth-state': 'Your login session expired. Please connect with Strava again.',
  'token-exchange-failed': 'We could not complete sign in with Strava. Please try again.',
};

export default async function HomePage({ searchParams }: HomePageProps) {
  const { error } = await searchParams;
  const errorMessage = error ? errorMessages[error] ?? 'Something went wrong. Please try again.' : null;

  return (
    <main className="container" style={{ display: 'grid', placeItems: 'center', minHeight: '100vh' }}>
      <section className="card" style={{ maxWidth: 680, padding: '2rem', textAlign: 'center' }}>
        <p style={{ fontSize: '0.9rem', margin: 0 }} className="muted">
          Route Type
        </p>
        <h1 style={{ margin: '0.8rem 0 0.6rem', fontSize: 'clamp(1.9rem, 4vw, 2.6rem)' }}>
          Turn your rides and runs into beautiful route typography.
        </h1>
        <p className="muted" style={{ marginBottom: '1.5rem', lineHeight: 1.6 }}>
          Connect your Strava account, choose an activity, and preview your route before generating your final design.
        </p>

        {errorMessage && (
          <p
            style={{
              margin: '0 0 1rem',
              color: '#8e2800',
              background: '#fff1ec',
              border: '1px solid #ffd8ca',
              borderRadius: 10,
              padding: '0.7rem 0.9rem',
            }}
          >
            {errorMessage}
          </p>
        )}

        <a className="btn" href="/api/strava/login">
          Connect with Strava
        </a>
      </section>
    </main>
  );
}
