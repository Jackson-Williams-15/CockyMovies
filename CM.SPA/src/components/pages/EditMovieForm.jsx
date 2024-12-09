import React from 'react';
import { Box, Grid, TextField, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const EditMovieForm = ({ editMovieData, handleEditMovieChange, handleEditMovieSubmit, genres, ratings }) => {
  return (
    <Box component="form" onSubmit={handleEditMovieSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            label="Title"
            name="title"
            value={editMovieData.title}
            onChange={handleEditMovieChange}
            fullWidth
            required
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Description"
            name="description"
            value={editMovieData.description}
            onChange={handleEditMovieChange}
            fullWidth
            multiline
            rows={4}
            required
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Image URL"
            name="imageUrl"
            value={editMovieData.imageUrl}
            onChange={handleEditMovieChange}
            fullWidth
            required
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth required>
            <InputLabel>Genre</InputLabel>
            <Select
              label="Genre"
              name="genreIds"
              value={editMovieData.genreIds}
              onChange={handleEditMovieChange}
              multiple
            >
              {genres.map((genre) => (
                <MenuItem key={genre.id} value={genre.id}>
                  {genre.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth required>
            <InputLabel>Rating</InputLabel>
            <Select
              label="Rating"
              name="ratingId"
              value={editMovieData.ratingId}
              onChange={handleEditMovieChange}
            >
              {ratings.map((rating) => (
                <MenuItem key={rating.id} value={rating.id}>
                  {rating.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <Button type="submit" variant="contained" color="primary" fullWidth>
            Edit Movie
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};

export default EditMovieForm;