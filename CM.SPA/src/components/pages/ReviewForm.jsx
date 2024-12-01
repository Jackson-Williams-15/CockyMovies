import React, { useState, useContext, useEffect } from 'react';
import { AuthContext } from '../../context/AuthContext';
import { addReview, editReview } from '../../Services/reviewService';
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
    if (!isLoggedIn) {
      setError('You must be logged in to submit a review.');
      return;
    }
    try {
      if (review) {
        await editReview(review.id, {
          title,
          rating,
          description,
        });
      } else {
        await addReview({
          title,
          rating,
          description,
          movieId: parseInt(movieId),
          username,
        });
      }
      setTitle('');
      setRating(0);
      setDescription('');
      setError(null);
      onSubmit &&
        onSubmit({
          id: review ? review.id : null,
          title,
          rating,
          description,
          movieId: parseInt(movieId),
          username,
        });
    } catch (error) {
      console.error('Failed to add review:', error);
      setError('Failed to add review. Please try again.');
    }
  };

  if (!isLoggedIn) {
    return (
      <Typography variant="body2" color="error" gutterBottom>
        You must be logged in to submit a review.
      </Typography>
    );
  }

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
