import axios from 'axios';

const API_BASE_URL = '/api/movies';

export const getMovies = async (genreIds = [], ratingIds = []) => {
  try {
    const params = {};
    if (genreIds.length > 0) {
      params.genreIds = genreIds.join(',');
    }
    if (ratingIds.length > 0) {
      params.ratingIds = ratingIds.join(',');
    }
    const response = await axios.get(API_BASE_URL, { params });
    return response.data;
  } catch (error) {
    console.error('Error fetching movies:', error);
    throw error;
  }
};

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