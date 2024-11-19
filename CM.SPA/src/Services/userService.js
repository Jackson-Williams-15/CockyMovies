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

// Update user information
const updateUserInfo = async (userData) => {
  const token = localStorage.getItem('token');
  if (!token) {
    throw new Error('No token found');
  }

  try {
    const response = await axios.put(`${BASE_API_URL}/update`, userData, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error(
      'Error updating user data:',
      error.response ? error.response.data : error.message,
    );
    throw error;
  }
};

// Update user password
const updatePassword = async (passwordData) => {
  const token = localStorage.getItem('token');
  if (!token) {
    throw new Error('No token found');
  }

  try {
    const response = await axios.put(
      `${BASE_API_URL}/update-password`,
      passwordData,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      },
    );
    return response.data;
  } catch (error) {
    console.error(
      'Error updating password:',
      error.response ? error.response.data : error.message,
    );
    throw error;
  }
};

// Save payment details
const savePaymentDetails = async (paymentDetails) => {
  const token = localStorage.getItem('token');
  if (!token) {
    throw new Error('No token found');
  }

  try {
    const response = await axios.post(
      `${BASE_API_URL}/save-payment-details`,
      paymentDetails,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      },
    );
    return response.data;
  } catch (error) {
    console.error(
      'Error saving payment details:',
      error.response ? error.response.data : error.message,
    );
    throw error;
  }
};

// Export the service functions
export default {
  getUserInfo,
  updateUserInfo,
  updatePassword,
  savePaymentDetails,
};
