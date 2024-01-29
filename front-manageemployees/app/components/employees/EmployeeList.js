"use client";

import { useEffect } from 'react';
import Employee from './Employee'; 
import { useEmployeeContext } from '@/app/contexts/EmployeeContext'; 

const EmployeeList = () => {
    const { employees, updateEmployees } = useEmployeeContext();

    useEffect(() => {
        updateEmployees();
    }, []);

    return (
        <div className='overflow-x-auto'>
            <table className='table w-full'>
                <thead>
                    <tr>
                        <th>Employés</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map((employee) => (
                        <Employee key={employee.id} employee={employee} />
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default EmployeeList;
