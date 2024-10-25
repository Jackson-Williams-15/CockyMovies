import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getShowtimes } from '../../Services/showtimeService';
import { getMovies } from '../../Services/movieService';

export default function Showtimes() {
  const { movieId } = useParams();
  const [showtimes, setShowtimes] = useState([]);
  const [movieTitle, setMovieTitle] = useState('');

  useEffect(() => {
    const fetchShowtimes = async () => {
      try {
        const showtimesData = await getShowtimes(movieId);
        setShowtimes(showtimesData);
      } catch (error) {
        console.error('Failed to fetch showtimes:', error);
      }
    };

    const fetchMovieTitle = async () => {
      try {
        const moviesData = await getMovies();
        const movie = moviesData.find(movie => movie.id === parseInt(movieId));
        if (movie) {
          setMovieTitle(movie.title);
        }
      } catch (error) {
        console.error('Failed to fetch movie title:', error);
      }
    };

    fetchShowtimes();
    fetchMovieTitle();
  }, [movieId]);

  return (
    <div>
      <h2>Showtimes for Movie: {movieTitle}</h2>
      <ul>
        {showtimes.map(showtime => (
          <li key={showtime.id}>{showtime.startTime}</li>
        ))}
      </ul>
    </div>
  );
}