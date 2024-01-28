import axios from 'axios';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const baseUrl = 'https://localhost:7282/api/Departments';

export const getAllDepartments = async () => {
  try {
    const response = await axios.get(baseUrl);
    return response.data;
  } catch (error) {
    console.error('Error fetching departments:', error);
    toast.error(`${error.response.data.detail}`);
  }
};

export const getOneDepartment = async (id) => {
  try {
    const response = await axios.get(`${baseUrl}/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching departments:', error);
    toast.error(`${error.response.data.detail}`);
  }
};

export const addDepartment = async (department) => {
  try {
    const response = await axios.post(baseUrl, department, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error adding department:', error);
    toast.error(`${error.response.data.detail}`);
  }
};

export const editDepartment = async (department) => {
  try {
    const response = await axios.put(`${baseUrl}/${department.id}`, department, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error editing department:', error);
    toast.error(`${error.response.data.detail}`);
  }
};

export const deleteDepartment = async (id) => {
  try {
    await axios.delete(`${baseUrl}/${id}`);
  } catch (error) {
    console.error('Error deleting department:', error);
    toast.error(`${error.response.data.detail}`);
  }
};
