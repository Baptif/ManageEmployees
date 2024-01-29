"use client";

import { useEffect, useState } from 'react';
import { deleteAttendance } from '@/api/attendanceApi';
import { getAllEmployees } from '@/api/employeeApi';
import { FiTrash2 } from "react-icons/fi";
import Modal from "../utils/Modal";
import { useAttendanceContext } from '@/app/contexts/AttendanceContext';

const AttendanceList = () => {
    const { attendanceList, selectedEmployeeId, updateAttendanceList, setSelectedEmployeeId } = useAttendanceContext();
  const [employeeOptions, setEmployeeOptions] = useState([]);
  const [openModalDeleted, setOpenModalDeleted] = useState(false);

  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const employees = await getAllEmployees();
        setEmployeeOptions(employees);
      } catch (error) {
        console.error('Error fetching employees:', error);
      }
    };

    fetchEmployees();
  }, []);

  useEffect(() => {
    const fetchAttendance = async () => {
      try {
          updateAttendanceList(selectedEmployeeId);
      } catch (error) {
        console.error('Error fetching attendance:', error);
      }
    };

    fetchAttendance();
  }, [selectedEmployeeId]);

    const handleDeleteAttendance = async (id) => {
        await deleteAttendance(id);
        updateAttendanceList(selectedEmployeeId);
        setOpenModalDeleted(false);
    };

  return (
    <div>
      <label>
        Sélectionner un employé:
        <select className="select" value={selectedEmployeeId} onChange={(e) => setSelectedEmployeeId(e.target.value)}>
          <option value="" >Choisissez un employé</option>
          {employeeOptions.map((employee) => (
            <option key={employee.id} value={employee.id}>
              {employee.firstName} - {employee.lastName}
            </option>
          ))}
        </select>
      </label>
      <div className='overflow-x-auto'>
        <table className='table w-full'>
            <thead>
            <tr>
                <th>Date de début</th>
                <th>Date de fin</th>
                <th>Actions</th>
            </tr>
            </thead>
            <tbody>
            {attendanceList.map((attendance) => (
                <tr>
                    <td>{attendance.startDate}</td>
                    <td>{attendance.endDate}</td>
                    <td>
                        <FiTrash2
                            onClick={() => setOpenModalDeleted(true)}
                            cursor='pointer'
                            className='text-red-500'
                            size={25}
                        />
                        <Modal modalOpen={openModalDeleted} setModalOpen={setOpenModalDeleted}>
                            <h3 className='text-lg'>
                                Êtes-vous sûr de vouloir supprimer la présence ?
                            </h3>
                            <div className='modal-action'>
                                <button onClick={() => handleDeleteAttendance(attendance.attendanceId)} className='btn'>
                                    Oui
                                </button>
                            </div>
                        </Modal>
                    </td>
                </tr>
            ))}
            </tbody>
        </table>
        </div>
    </div>
  );
};

export default AttendanceList;
