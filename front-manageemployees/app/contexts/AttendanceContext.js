"use client";

import { createContext, useContext, useState } from 'react';
import { getAttendanceByEmployeeId } from '@/api/attendanceApi';

const AttendanceContext = createContext();

export const AttendanceProvider = ({ children }) => {
  const [attendanceList, setAttendanceList] = useState([]);
  const [selectedEmployeeId, setSelectedEmployeeId] = useState(null);

  const updateAttendanceList = async (employeeId) => {
    try {
      if (employeeId) {
        const attendanceData = await getAttendanceByEmployeeId(employeeId);
        setAttendanceList(attendanceData);
      } else {
        setAttendanceList([]);
      }
    } catch (error) {
      console.error('Error updating attendance list:', error);
    }
  };

  return (
    <AttendanceContext.Provider value={{ attendanceList, selectedEmployeeId, updateAttendanceList, setSelectedEmployeeId }}>
      {children}
    </AttendanceContext.Provider>
  );
};

export const useAttendanceContext = () => {
  return useContext(AttendanceContext);
};
