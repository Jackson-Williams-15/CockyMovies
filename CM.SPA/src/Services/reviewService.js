export async function getReviews(movieId) {
    const BASE_API_URL = `/api/reviews/movie`;
  
    const response = await fetch(`${BASE_API_URL}/${movieId}`);
    if (!response.ok) {
      throw new Error('Failed to fetch reviews');
    }
    return response.json();
  }