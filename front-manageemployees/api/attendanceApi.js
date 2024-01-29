import axios from 'axios';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { getErrorMsg } from './functionUtils';

const baseUrl = 'https://localhost:7282/api/Attendance';

export const getAttendanceByEmployeeId = async (employeeId) => {
  try {
    const response = await axios.get(`${baseUrl}/employee/${employeeId}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching attendance:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const getAttendanceById = async (attendanceId) => {
  try {
    const response = await axios.get(`${baseUrl}/${attendanceId}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching attendance details:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const createAttendance = async (attendance) => {
  try {
    const response = await axios.post(baseUrl, attendance, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error creating attendance:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};

export const deleteAttendance = async (id) => {
  try {
    await axios.delete(`${baseUrl}/${id}`);
  } catch (error) {
    console.error('Error deleting attendance:', error);
    toast.error(`${getErrorMsg(error)}`);
  }
};
