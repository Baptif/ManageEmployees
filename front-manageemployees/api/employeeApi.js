import axios from 'axios';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { getErrorMsg } from './functionUtils';

const baseUrl = 'https://localhost:7282/api/Employees';

export const getAllEmployees = async () => {
  try {
    const response = await axios.get(baseUrl);
    return response.data;
  } catch (error) {
    console.error('Error fetching employees:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const getOneEmployee = async (id) => {
  try {
    const response = await axios.get(`${baseUrl}/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching employee:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const addEmployee = async (employee) => {
  try {
    const response = await axios.post(baseUrl, employee, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error adding employee:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const editEmployee = async (employee) => {
  try {
    const response = await axios.put(`${baseUrl}/${employee.id}`, employee, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error editing employee:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const deleteEmployee = async (id) => {
  try {
    await axios.delete(`${baseUrl}/${id}`);
  } catch (error) {
    console.error('Error deleting employee:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

// Exemple de fonctions supplémentaires liées aux départements pour montrer comment ajouter des fonctionnalités spécifiques si nécessaire

export const getDepartmentsForEmployee = async (employeeId) => {
  try {
    const response = await axios.get(`${baseUrl}/${employeeId}/departments`);
    return response.data;
  } catch (error) {
    console.error('Error fetching departments for employee:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const addDepartmentToEmployee = async (employeeId, departmentId) => {
  try {
    const response = await axios.put(`${baseUrl}/${employeeId}/departments/${departmentId}`);
    return response.data;
  } catch (error) {
    console.error('Error adding department to employee:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const removeDepartmentFromEmployee = async (employeeId, departmentId) => {
  try {
    const response = await axios.delete(`${baseUrl}/${employeeId}/departments/${departmentId}`);
    return response.data;
  } catch (error) {
    console.error('Error removing department from employee:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};