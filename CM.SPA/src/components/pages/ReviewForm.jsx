import React, { useState, useContext } from 'react';
import { AuthContext } from '../../context/AuthContext';
import { addReview } from '../../Services/reviewService';
import { useParams } from 'react-router-dom';
import { TextField, Button, Box, Typography, Rating } from '@mui/material';

export default function ReviewForm() {
  const { movieId } = useParams();
  const { username } = useContext(AuthContext); // Get the current user's username
  const [title, setTitle] = useState('');
  const [rating, setRating] = useState(0);
  const [description, setDescription] = useState('');
  const [error, setError] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await addReview({
        title,
        rating,
        description,
        movieId: parseInt(movieId),
        username,
      });
      
      setTitle('');
      setRating(0);
      setDescription('');
      setError(null);
    } catch (error) {
      console.error('Failed to add review:', error);
      setError('Failed to add review. Please try again.');
    }
  };

  return (
    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 3 }}>
      <Typography variant="h6" gutterBottom>
        Add a Review
      </Typography>
      {error && (
        <Typography variant="body2" color="error" gutterBottom>
          {error}
        </Typography>
      )}
      <TextField
        label="Title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        fullWidth
        margin="normal"
        required
      />
      <Rating
        value={rating}
        onChange={(e, newValue) => setRating(newValue)}
        precision={0.5}
        required
      />
      <TextField
        label="Description"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        fullWidth
        margin="normal"
        multiline
        rows={4}
        required
      />
      <Button type="submit" variant="contained" color="primary" sx={{ mt: 2 }}>
        Submit Review
      </Button>
    </Box>
  );
}