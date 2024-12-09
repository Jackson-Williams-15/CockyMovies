import React from 'react';
import { Box, Grid, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const RemoveMovieForm = ({ removeMovieData, handleRemoveMovieChange, handleRemoveMovieSubmit, movies }) => {
  return (
    <Box component="form" onSubmit={handleRemoveMovieSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <FormControl fullWidth required>
            <InputLabel>Movie</InputLabel>
            <Select
              label="Movie"
              name="id"
              value={removeMovieData.id}
              onChange={handleRemoveMovieChange}
            >
              <MenuItem value="">
                <em>None</em>
              </MenuItem>
              {movies.map((movie) => (
                <MenuItem key={movie.id} value={movie.id}>
                  {movie.title}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <Button type="submit" variant="contained" color="primary" fullWidth>
            Remove Movie
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};

export default RemoveMovieForm;