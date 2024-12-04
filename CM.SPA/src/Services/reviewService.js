import axios from 'axios';

export async function getReviews(movieId) {
  const BASE_API_URL = `/api/reviews/movie`;

  const response = await fetch(`${BASE_API_URL}/${movieId}`);
  if (!response.ok) {
    throw new Error('Failed to fetch reviews');
  }
  return response.json();
}

export async function addReview(review) {
  try {
    const response = await axios.post('/api/reviews', review);
    return response.data;
  } catch (error) {
    console.error('Failed to add review:', error);
    throw error;
  }
}

export async function editReview(reviewId, review) {
  try {
    const response = await axios.put(`/api/reviews/${reviewId}`, review);
    return response.data;
  } catch (error) {
    console.error('Failed to edit review:', error);
    throw error;
  }
}

export async function removeReview(reviewId) {
  try {
    const response = await axios.delete(`/api/reviews/${reviewId}`);
    return response.data;
  } catch (error) {
    console.error('Failed to delete review:', error);
    throw error;
  }
}

export async function likeReview(reviewId) {
  try {
    const response = await axios.post(`/api/reviews/${reviewId}/like`);
    return response.data;
  } catch (error) {
    console.error('Failed to like review:', error);
    throw error;
  }
}