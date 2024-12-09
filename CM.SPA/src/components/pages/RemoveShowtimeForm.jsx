import React from 'react';
import { Box, Grid, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const RemoveShowtimeForm = ({ removeShowtimeData, handleRemoveShowtimeChange, handleRemoveShowtimeSubmit, movies, showtimes }) => {
  return (
    <Box component="form" onSubmit={handleRemoveShowtimeSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <FormControl fullWidth required>
            <InputLabel>Movie</InputLabel>
            <Select
              label="Movie"
              name="movieId"
              value={removeShowtimeData.movieId}
              onChange={handleRemoveShowtimeChange}
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
          <FormControl fullWidth required>
            <InputLabel>Showtime</InputLabel>
            <Select
              label="Showtime"
              name="id"
              value={removeShowtimeData.id}
              onChange={handleRemoveShowtimeChange}
              disabled={!removeShowtimeData.movieId} // Disable if no movie is selected
            >
              <MenuItem value="">
                <em>None</em>
              </MenuItem>
              {showtimes.map((showtime) => (
                <MenuItem key={showtime.id} value={showtime.id}>
                  {new Date(showtime.startTime).toLocaleString('en-US', {
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric',
                    hour: 'numeric',
                    minute: 'numeric',
                    hour12: true,
                  })}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
        <Grid item xs={12}>
          <Button type="submit" variant="contained" color="primary" fullWidth>
            Remove Showtime
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};

export default RemoveShowtimeForm;