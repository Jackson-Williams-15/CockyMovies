import axios from 'axios';

const API_BASE_URL = '/api/movies';

export const getMovies = async (genreIds = [], ratingIds = []) => {
  try {
    const params = new URLSearchParams();
    genreIds.forEach(id => params.append('genreIds', id));
    ratingIds.forEach(id => params.append('ratingIds', id));
    const response = await axios.get(`${API_BASE_URL}?${params.toString()}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching movies:', error);
    throw error;
  }
};

export async function getMovieById(movieId) {
  const BASE_API_URL = `/api/movies`;

  const response = await fetch(`${BASE_API_URL}/${movieId}`);
  if (!response.ok) {
    throw new Error('Failed to fetch movie');
  }
  return response.json();
}

export const getGenres = async () => {
  try {
    const response = await axios.get('/api/genre');
    return response.data;
  } catch (error) {
    console.error('Error fetching genres:', error);
    throw error;
  }
};

export const getRatings = async () => {
  try {
    const response = await axios.get('/api/ratings');
    return response.data;
  } catch (error) {
    console.error('Error fetching ratings:', error);
    throw error;
  }
};