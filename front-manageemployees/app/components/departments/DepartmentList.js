"use client";

import { useEffect } from 'react';
import Department from './Department';
import { useDepartmentContext } from '@/app/contexts/DepartmentContext';

const DepartmentList = () => {
    const { departments, updateDepartments } = useDepartmentContext()

    useEffect(() => {
        updateDepartments();
    }, []);

    return (
        <div className='overflow-x-auto'>
        <table className='table w-full'>
            <thead>
            <tr>
                <th>Departements</th>
                <th>Actions</th>
            </tr>
            </thead>
            <tbody>
            {departments.map((department) => (
                <Department key={department.departmentId} department={department} />
            ))}
            </tbody>
        </table>
        </div>
    );
};

export default DepartmentList;