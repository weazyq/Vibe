import { Box, Button, Typography } from "@mui/material"
import { Employee } from "../../domain/employees/employee"
import CModal from "../../sharedComponents/modal"

interface EmployeeDeleteModalProps {
    employee: Employee
    onDelete: (employeeId: string) => void
    onClose: () => void
}

function EmployeeDeleteModal(props: EmployeeDeleteModalProps) {

    function handleDeleteEmployee(){
        props.onDelete(props.employee.id)
        props.onClose()
    }

  return (
    <CModal
        title={"Удаление сотрудника"}
        actions={
            <>
                <Button variant="outlined" onClick={props.onClose}>Отменить</Button>
                <Button variant="contained" onClick={handleDeleteEmployee}>Подтвердить</Button>
            </>
        }
        onClose={props.onClose}>
        <Box>
            <Typography variant="body2">Вы действительно хотите подтвердить удаление учётной записи сотрудника {props.employee.name} ({props.employee.login})</Typography>
        </Box>
    </CModal>
  )
}

export default EmployeeDeleteModal