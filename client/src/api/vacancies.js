import axios from 'axios';

const API_BASE = 'https://a86a-217-19-215-68.ngrok-free.app/api/vacancies';

export async function getAllVacancies() {
  const response = await axios.get(API_BASE, {
    headers: {
        'ngrok-skip-browser-warning': 'true'
    }
  });
  return response.data;
}

export async function searchVacancies(searchDto) {
  const response = await axios.post(`${API_BASE}/search`, searchDto, {
    headers: {
        'ngrok-skip-browser-warning': 'true'
    }
  });
  return response.data;
}

export async function getVacancyById(id) {
  const response = await axios.get(`${API_BASE}/${id}`, {
    headers: {
        'ngrok-skip-browser-warning': 'true'
    }
  });
  return response.data;
}

export async function createVacancy(vacancyDto) {
  const response = await axios.post(API_BASE, vacancyDto, {
    headers: {
        'ngrok-skip-browser-warning': 'true'
    }
  });
  return response.data;
}

export async function updateVacancy(id, vacancyDto) {
  const response = await axios.put(`${API_BASE}/${id}`, vacancyDto, {
    headers: {
        'ngrok-skip-browser-warning': 'true'
    }
  });
  return response.data;
}

export async function deleteVacancy(id) {
  const response = await axios.delete(`${API_BASE}/${id}`, {
    headers: {
        'ngrok-skip-browser-warning': 'true'
    }
  });
  return response.data;
}