import axios from 'axios';

const BASE_API_URL = '/api/Account';

// Get user information
const getUserInfo = async () => {
  const token = localStorage.getItem('token');
  if (!token) {
    throw new Error('No token found');
  }

  try {
    const response = await axios.get(`${BASE_API_URL}/profile`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error(
      'Error fetching user data:',
      error.response ? error.response.data : error.message,
    );
    throw error;
  }
};

// Export the service functions
export default {
  getUserInfo,
};
