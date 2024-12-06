
import axios from 'axios';

export async function addReply(reviewId, reply) {
    try {
      const response = await axios.post(`/api/reviews/${reviewId}/replies`, reply);
      return response.data;
    } catch (error) {
      console.error('Failed to add reply:', error);
      throw error;
    }
  }
  
  export async function getReplies(reviewId) {
    try {
      const response = await axios.get(`/api/reviews/${reviewId}/replies`);
      return response.data;
    } catch (error) {
      console.error('Failed to fetch replies:', error);
      throw error;
    }
  }