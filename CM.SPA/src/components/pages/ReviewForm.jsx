import React, { useState, useContext, useEffect } from 'react';
import { AuthContext } from '../../context/AuthContext';
import { useParams } from 'react-router-dom';
import { TextField, Button, Box, Typography, Rating } from '@mui/material';

export default function ReviewForm({ review, onSubmit, onCancel }) {
  const { movieId } = useParams();
  const { username, isLoggedIn } = useContext(AuthContext);
  const [title, setTitle] = useState(review ? review.title : '');
  const [rating, setRating] = useState(review ? review.rating : 0);
  const [description, setDescription] = useState(
    review ? review.description : '',
  );
  const [error, setError] = useState(null);

  useEffect(() => {
    if (review) {
      setTitle(review.title);
      setRating(review.rating);
      setDescription(review.description);
    }
  }, [review]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const reviewUsername = isLoggedIn ? username : 'Anonymous';
    try {
      const newReview = {
        title,
        rating,
        description,
        movieId: parseInt(movieId),
        username: reviewUsername,
      };
      onSubmit && onSubmit(newReview);
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
        {review ? 'Edit Review' : 'Add a Review'}
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
        {review ? 'Update Review' : 'Submit Review'}
      </Button>
      {onCancel && (
        <Button
          onClick={onCancel}
          variant="contained"
          color="secondary"
          sx={{ mt: 2, ml: 2 }}
        >
          Cancel
        </Button>
      )}
    </Box>
  );
}