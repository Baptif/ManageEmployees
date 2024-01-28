"use client";

import { getAllDepartments } from '@/api/departmentApi';
import { createContext, useContext, useState } from 'react';

const DepartmentContext = createContext();

export const DepartmentProvider = ({ children }) => {
  const [departments, setDepartments] = useState([]);

  const updateDepartments = async () => {
    const fetchedDepartments = await getAllDepartments();
    setDepartments(fetchedDepartments);
  };

  return (
    <DepartmentContext.Provider value={{ departments, updateDepartments }}>
      {children}
    </DepartmentContext.Provider>
  );
};

export const useDepartmentContext = () => {
  return useContext(DepartmentContext);
};
