"use client";

import { useState, useEffect } from "react";
import { FiEdit, FiTrash2, FiFileText } from "react-icons/fi";
import Modal from "../utils/Modal";
import { deleteEmployee, editEmployee, getOneEmployee } from "@/api/employeeApi";
import { useEmployeeContext } from "@/app/contexts/EmployeeContext";
import EmployeeDetails from "./EmployeeDetails";

const Employee = ({ employee }) => {
    const { updateEmployees } = useEmployeeContext();

    const [openModalDetailDepart, setOpenModalDetailDepart] = useState(false);
    const [openModalDetail, setOpenModalDetail] = useState(false);
    const [openModalEdit, setOpenModalEdit] = useState(false);
    const [openModalDeleted, setOpenModalDeleted] = useState(false);

    const [formFirstNameValue, setFormFirstNameValue] = useState("");
    const [formLastNameValue, setFormLastNameValue] = useState("");
    const [formPositionValue, setFormPositionValue] = useState("");
    const [formBirthDateValue, setFormBirthDateValue] = useState("");
    const [formEmailValue, setFormEmailValue] = useState("");
    const [formPhoneNumberValue, setFormPhoneNumberValue] = useState("");

    useEffect(() => {
        const loadEmployeeDetails = async () => {
            try {
                const details = await getOneEmployee(employee.id);
                const formattedBirthDate = new Date(details.birthDate).toISOString().split('T')[0];
                setFormFirstNameValue(details.firstName);
                setFormLastNameValue(details.lastName);
                setFormPositionValue(details.position);
                setFormBirthDateValue(formattedBirthDate);
                setFormEmailValue(details.email);
                setFormPhoneNumberValue(details.phoneNumber);
            } catch (error) {
                console.error('Error loading employee details:', error);
            }
        };

        loadEmployeeDetails();
    }, []);

    const handleSubmitEditEmployee = async (e) => {
        e.preventDefault();
        await editEmployee({
            id: employee.id,
            firstName: formFirstNameValue,
            lastName: formLastNameValue,
            position: formPositionValue,
            birthDate: formBirthDateValue,
            email: formEmailValue,
            phoneNumber: formPhoneNumberValue,
        });
        setOpenModalEdit(false);
        updateEmployees();
    };

    const handleDeleteEmployee = async (id) => {
        await deleteEmployee(id);
        setOpenModalDeleted(false);
        updateEmployees();
    };

    return (
        <tr key={employee.id}>
            <td className='w-full'>{employee.firstName} - {employee.lastName} - {employee.email}</td>
            <td className='flex gap-5'>
                <FiFileText
                    onClick={() => setOpenModalDetailDepart(true)}
                    cursor='pointer'
                    className='text-orange-500'
                    size={25}
                />
                <Modal modalOpen={openModalDetailDepart} setModalOpen={setOpenModalDetailDepart}>
                    <EmployeeDetails employeeId={employee.id}/>
                </Modal>
                <FiFileText
                    onClick={() => setOpenModalDetail(true)}
                    cursor='pointer'
                    className='text-green-500'
                    size={25}
                />
                <Modal modalOpen={openModalDetail} setModalOpen={setOpenModalDetail}>
                    <h3 className='font-bold text-lg'>Détails de l'employé</h3>
                    <p>Prénom: {formFirstNameValue}</p>
                    <p>Nom: {formLastNameValue}</p>
                    <p>Poste: {formPositionValue}</p>
                    <p>Date de naissance: {formBirthDateValue}</p>
                    <p>Email: {formEmailValue}</p>
                    <p>Numéro de téléphone: {formPhoneNumberValue}</p>
                </Modal>
                <FiEdit
                    onClick={() => setOpenModalEdit(true)}
                    cursor='pointer'
                    className='text-blue-500'
                    size={25}
                />
                <Modal modalOpen={openModalEdit} setModalOpen={setOpenModalEdit}>
                    <form onSubmit={handleSubmitEditEmployee}>
                        <h3 className='font-bold text-lg'>Modifier l'employé</h3>

                        <label className="form-control w-full">
                            <div className="label">
                                <span className="label-text">Prénom</span>
                            </div>
                            <input
                                value={formFirstNameValue}
                                onChange={(e) => setFormFirstNameValue(e.target.value)}
                                type="text"
                                placeholder="Taper ici..."
                                className="input input-bordered w-full"
                            />
                        </label>

                        <label className="form-control w-full">
                            <div className="label">
                                <span className="label-text">Nom</span>
                            </div>
                            <input
                                value={formLastNameValue}
                                onChange={(e) => setFormLastNameValue(e.target.value)}
                                type="text"
                                placeholder="Taper ici..."
                                className="input input-bordered w-full"
                            />
                        </label>

                        <label className="form-control w-full">
                            <div className="label">
                                <span className="label-text">Poste</span>
                            </div>
                            <input
                                value={formPositionValue}
                                onChange={(e) => setFormPositionValue(e.target.value)}
                                type="text"
                                placeholder="Taper ici..."
                                className="input input-bordered w-full"
                            />
                        </label>

                        <label className="form-control w-full">
                            <div className="label">
                                <span className="label-text">Date de naissance</span>
                            </div>
                            <input
                                value={formBirthDateValue}
                                onChange={(e) => setFormBirthDateValue(e.target.value)}
                                type="date"
                                placeholder="Sélectionner la date"
                                className="input input-bordered w-full"
                            />
                        </label>

                        <label className="form-control w-full">
                            <div className="label">
                                <span className="label-text">Email</span>
                            </div>
                            <input
                                value={formEmailValue}
                                onChange={(e) => setFormEmailValue(e.target.value)}
                                type="email"
                                placeholder="Taper ici..."
                                className="input input-bordered w-full"
                            />
                        </label>

                        <label className="form-control w-full mb-8">
                            <div className="label">
                                <span className="label-text">Numéro de téléphone</span>
                            </div>
                            <input
                                value={formPhoneNumberValue}
                                onChange={(e) => setFormPhoneNumberValue(e.target.value)}
                                type="tel"
                                placeholder="Taper ici..."
                                className="input input-bordered w-full"
                            />
                        </label>

                        <button type="submit" className="btn w-full">
                            Modifier
                        </button>
                    </form>
                </Modal>
                <FiTrash2
                    onClick={() => setOpenModalDeleted(true)}
                    cursor='pointer'
                    className='text-red-500'
                    size={25}
                />
                <Modal modalOpen={openModalDeleted} setModalOpen={setOpenModalDeleted}>
                    <h3 className='text-lg'>
                        Êtes-vous sûr de vouloir supprimer l'employé {employee.firstName} {employee.lastName} ?
                    </h3>
                    <div className='modal-action'>
                        <button onClick={() => handleDeleteEmployee(employee.id)} className='btn'>
                            Oui
                        </button>
                    </div>
                </Modal>
            </td>
        </tr>
    );
};

export default Employee;
