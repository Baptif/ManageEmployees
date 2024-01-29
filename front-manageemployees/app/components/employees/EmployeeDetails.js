import { useEffect, useState } from 'react';
import { getDepartmentsForEmployee, addDepartmentToEmployee, removeDepartmentFromEmployee } from '@/api/employeeApi';
import { getAllDepartments } from '@/api/departmentApi';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const EmployeeDetails = ({ employeeId }) => {
  const [departments, setDepartments] = useState([]);
  const [selectedDepartment, setSelectedDepartment] = useState("");
  const [allDepartments, setAllDepartments] = useState([]);

  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const fetchedDepartments = await getDepartmentsForEmployee(employeeId);
        setDepartments(fetchedDepartments);
      } catch (error) {
        console.error('Error fetching departments for employee:', error);
      }
    };
    const fetchAllDepartments = async () => {
        try {
          const allDepartmentsData = await getAllDepartments();
          setAllDepartments(allDepartmentsData);
        } catch (error) {
          console.error('Error fetching all departments:', error);
        }
    };
  
    fetchAllDepartments();
    fetchDepartments();
  }, []);

  const handleAddDepartment = async () => {
    try {
        if (selectedDepartment) {
            await addDepartmentToEmployee(employeeId, selectedDepartment);
            const updatedDepartments = await getDepartmentsForEmployee(employeeId);
            setDepartments(updatedDepartments);
        } else {
            toast.error("Sélectionner un département avant d'ajouter");
        }
    } catch (error) {
    console.error('Error adding department to employee:', error);
    }
};

  const handleRemoveDepartment = async (departmentId) => {
    try {
      await removeDepartmentFromEmployee(employeeId, departmentId);
      const updatedDepartments = await getDepartmentsForEmployee(employeeId);
      setDepartments(updatedDepartments);
    } catch (error) {
      console.error('Error removing department from employee:', error);
    }
  };

  return (
    <div>
      <h3 className='font-bold text-lg mb-2'>Départements de l'employé</h3>
      <ul>
        {departments.map((department) => (
          <li key={department.departmentId} className='mt-1'>
            <p>{department.name}</p>
            <button onClick={() => handleRemoveDepartment(department.departmentId)} className='btn btn-neutral btn-xs mb-2'>
              Supprimer
            </button>
          </li>
        ))}
      </ul>

      <hr></hr>
      <select className="select mt-2 mr-2" value={selectedDepartment} onChange={(e) => setSelectedDepartment(e.target.value)}>
        <option value="">Sélectionner un département</option>
        {allDepartments.map((dept) => (
          <option key={dept.departmentId} value={dept.departmentId}>
            {dept.name}
          </option>
        ))}
      </select>
      <button className='btn btn-sm mt-2' onClick={handleAddDepartment}>Ajouter un département</button>
    </div>
  );
};

export default EmployeeDetails;