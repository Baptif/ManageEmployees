"use client";

import { getAllEmployees } from '@/api/employeeApi';
import { createContext, useContext, useState } from 'react';

const EmployeeContext = createContext();

export const EmployeeProvider = ({ children }) => {
  const [employees, setEmployees] = useState([]);

  const updateEmployees = async () => {
    const fetchedEmployees = await getAllEmployees();
    setEmployees(fetchedEmployees);
  };

  return (
    <EmployeeContext.Provider value={{ employees, updateEmployees }}>
      {children}
    </EmployeeContext.Provider>
  );
};

export const useEmployeeContext = () => {
  return useContext(EmployeeContext);
};