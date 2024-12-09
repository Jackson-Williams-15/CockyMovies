import React from 'react';
import { Box, Grid, TextField, Button, FormControl, InputLabel, Select, MenuItem, Checkbox, ListItemText } from '@mui/material';

const MovieForm = ({ formData, handleChange, handleGenreChange, handleMovieSubmit, genres, ratings }) => {
  return (
    <Box component="form" onSubmit={handleMovieSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <TextField
            label="Title"
            name="title"
            value={formData.title}
            onChange={handleChange}
            fullWidth
            required
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Description"
            name="description"
            value={formData.description}
            onChange={handleChange}
            fullWidth
            required
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Date Released"
            name="dateReleased"
            type="date"
            value={formData.dateReleased}
            onChange={handleChange}
            fullWidth
            InputLabelProps={{
              shrink: true,
            }}
            required
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Image URL"
            name="imageUrl"
            value={formData.imageUrl}
            onChange={handleChange}
            fullWidth
            required
          />
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth required>
            <InputLabel>Rating</InputLabel>
            <Select
              label="Rating"
              name="ratingId"
              value={formData.ratingId}
              onChange={handleChange}
            >
              <MenuItem value="">
                <em>None</em>
              </MenuItem>
              {ratings.map((rating) => (
                <MenuItem key={rating.id} value={rating.id}>
                  {rating.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <FormControl fullWidth required>
            <InputLabel>Genres</InputLabel>
            <Select
              label="Genres"
              name="genreIds"
              multiple
              value={formData.genreIds}
              onChange={handleGenreChange}
              renderValue={(selected) => selected.map(id => genres.find(g => g.id === id)?.name).join(', ')}
            >
              {genres.map((genre) => (
                <MenuItem key={genre.id} value={genre.id}>
                  <Checkbox checked={formData.genreIds.indexOf(genre.id) > -1} />
                  <ListItemText primary={genre.name} />
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <Button type="submit" variant="contained" color="primary" fullWidth>
            Add Movie
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};

export default MovieForm;