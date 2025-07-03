import React, { useEffect, useState } from 'react';
import axios from 'axios';

function MoviesList() {
  const [movies, setMovies] = useState([]);

    
  useEffect(() => {
    axios.get('https://032a-80-94-250-53.ngrok-free.app/api/movies', {
      headers: {
        'ngrok-skip-browser-warning': 'true'
      }
    })
    .then(res => {
      console.log('Response status:', res.status);
      setMovies(res.data);
    })
    .catch(err => {
      if (err.response) {
        // Сервер ответил с ошибкой
        console.error('Ошибка сервера:', err.response.status, err.response.data);
      } else if (err.request) {
        // Запрос был сделан, но ответа нет
        console.error('Нет ответа от сервера:', err.request);
      } else {
        // Другая ошибка
        console.error('Ошибка:', err.message);
      }
    });
  }, []);


  return (
    <div>
      <h2>Фильмы</h2>
      <ul>
        {movies.map(movie => <li key={movie.id}>{movie.title}</li>)}
      </ul>
    </div>
  );
}

export default MoviesList;