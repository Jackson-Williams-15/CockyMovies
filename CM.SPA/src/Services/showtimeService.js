export async function getShowtimes(movieId) {
  const BASE_API_URL = `/api/showtimes`;

  const response = await fetch(`${BASE_API_URL}/movie/${movieId}`);
  if (!response.ok) {
    throw new Error('Failed to fetch showtimes');
  }
  return response.json();
}
