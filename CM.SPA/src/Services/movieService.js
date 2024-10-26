import axios from 'axios';

const API_BASE_URL = '/api/movies';

export const getMovies = async () => {
  try {
    const response = await axios.get(API_BASE_URL);
    return response.data;
  } catch (error) {
    console.error('Error fetching movies:', error);
    throw error;
  }
};
