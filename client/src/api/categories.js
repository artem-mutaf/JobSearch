import axios from 'axios';

const API_URL = 'https://a86a-217-19-215-68.ngrok-free.app/api/categories'; 

export const getAllCategories = async () => {
  const response = await axios.get(API_URL, {
    headers: {
        'ngrok-skip-browser-warning': 'true'
    }
  });
   
  return response.data;
};

export const createCategory = async (data) => {
  const response = await axios.post(API_URL, data);
  return response.data;
};