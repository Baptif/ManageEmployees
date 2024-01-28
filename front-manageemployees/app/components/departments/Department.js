"use client";

import { useState, useEffect } from "react";
import { FiEdit, FiTrash2, FiFileText  } from "react-icons/fi";
import Modal from "../utils/Modal";
import { deleteDepartment, editDepartment, getOneDepartment } from "@/api/departmentApi";
import { useDepartmentContext } from "@/app/contexts/DepartmentContext";

const Department = ({ department }) => {
    const { updateDepartments } = useDepartmentContext();
    
    const [openModalDetail, setOpenModalDetail] = useState(false);
    const [openModalEdit, setOpenModalEdit] = useState(false);
    const [openModalDeleted, setOpenModalDeleted] = useState(false);

    const [formNameValue, setFormNameValue] = useState("");
    const [formDescriptionValue, setFormDescriptionValue] = useState("");
    const [formAdresseValue, setFormAdresseValue] = useState("");

    useEffect(() => {
        const loadDepartmentDetails = async () => {
            try {
                const details = await getOneDepartment(department.departmentId);
                setFormNameValue(details.name);
                setFormDescriptionValue(details.description);
                setFormAdresseValue(details.address);
            } catch (error) {
                console.error('Error loading department details:', error);
            }
        };

        loadDepartmentDetails();
    }, []);

    const handleSubmitEditTodo = async (e) => {
        e.preventDefault();
        await editDepartment({
            id: department.departmentId,
            name: formNameValue,
            description: formDescriptionValue,
            address: formAdresseValue,
        });
        setOpenModalEdit(false);
        updateDepartments();
    };

    const handleDeleteTask = async (id) => {
        await deleteDepartment(id);
        setOpenModalDeleted(false);
        updateDepartments();
    };

    return (
        <tr key={department.departmentId}>
            <td className='w-full'>{department.name}</td>
            <td className='flex gap-5'>
                <FiFileText
                    onClick={() => setOpenModalDetail(true)}
                    cursor='pointer'
                    className='text-green-500'
                    size={25}
                />
                <Modal modalOpen={openModalDetail} setModalOpen={setOpenModalDetail}>
                    <h3 className='font-bold text-lg'>Détails du département</h3>
                    <p>Nom: {formNameValue}</p>
                    <p>Description: {formDescriptionValue}</p>
                    <p>Adresse: {formAdresseValue}</p>
                </Modal>
                <FiEdit
                    onClick={() => setOpenModalEdit(true)}
                    cursor='pointer'
                    className='text-blue-500'
                    size={25}
                />
                <Modal modalOpen={openModalEdit} setModalOpen={setOpenModalEdit}>
                    <form onSubmit={handleSubmitEditTodo}>
                        <h3 className='font-bold text-lg'>Modifier le département</h3>
                        <label className="form-control w-full">

                        <div className="label">
                            <span className="label-text">Nom</span>
                        </div>
                        <input
                            value={formNameValue}
                            onChange={(e) => setFormNameValue(e.target.value)}
                            type="text"
                            placeholder="Taper ici..."
                            className="input input-bordered w-full"
                        />
                        </label>

                        <label className="form-control w-full">
                            <div className="label">
                                <span className="label-text">Description</span>
                            </div>
                            <input
                                value={formDescriptionValue}
                                onChange={(e) => setFormDescriptionValue(e.target.value)}
                                type="text"
                                placeholder="Taper ici..."
                                className="input input-bordered w-full"
                            />
                        </label>

                        <label className="form-control w-full mb-8">
                            <div className="label">
                                <span className="label-text">Adresse</span>
                            </div>
                            <input
                                value={formAdresseValue}
                                onChange={(e) => setFormAdresseValue(e.target.value)}
                                type="text"
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
                        Etes vous sur de vouloir supprimer le departement {department.name} ?
                    </h3>
                    <div className='modal-action'>
                        <button onClick={() => handleDeleteTask(department.departmentId)} className='btn'>
                            Oui
                        </button>
                    </div>
                </Modal>
            </td>
        </tr>
    );
};

export default Department;