import React from 'react';
import { Box, Grid, TextField, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const ShowtimeForm = ({ showtimeData, handleShowtimeChange, handleShowtimeSubmit, movies }) => {
  return (
    <Box component="form" onSubmit={handleShowtimeSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <FormControl fullWidth required>
            <InputLabel>Movie</InputLabel>
            <Select
              label="Movie"
              name="movieId"
              value={showtimeData.movieId}
              onChange={handleShowtimeChange}
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
          <TextField
            label="Start Time"
            name="startTime"
            type="datetime-local"
            value={showtimeData.startTime}
            onChange={handleShowtimeChange}
            fullWidth
            InputLabelProps={{
              shrink: true,
            }}
            required
          />
        </Grid>
        <Grid item xs={12}>
          <TextField
            label="Capacity"
            name="capacity"
            type="number"
            value={showtimeData.capacity}
            onChange={handleShowtimeChange}
            fullWidth
            required
          />
        </Grid>
        <Grid item xs={12}>
          <Button type="submit" variant="contained" color="primary" fullWidth>
            Add Showtime
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};

export default ShowtimeForm;