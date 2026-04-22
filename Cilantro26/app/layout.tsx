import './globals.css';
import type { Metadata } from 'next';

export const metadata: Metadata = {
  title: 'Route Type',
  description: 'Turn your Strava routes into visual typography art.',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}
