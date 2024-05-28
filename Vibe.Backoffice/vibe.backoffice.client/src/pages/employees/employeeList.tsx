import { Box, Card, CardContent, Divider, Grid, IconButton, Stack, Typography } from "@mui/material"
import {AccountCircle, Phone, Email, Edit, Delete} from "@mui/icons-material"
import { EmployeeProvider } from "../../domain/employees/employeeProvider"
import { Employee } from "../../domain/employees/employee"
import { ReactElement, useEffect, useState } from "react"
import useModal from "../../hooks/useModal"
import EmployeeDeleteModal from "./employeeDeleteModal"
import Result from "../../tools/result"

function EmployeesList() {

  const [employees, setEmployees] = useState<Employee[]>([])
  const [modal, showModal, hideModal] = useModal<ReactElement>()

  async function loadEmployees(){
    const employees = await getEmployees()
    setEmployees(employees)
  }

  useEffect(() => {
    loadEmployees()
  }, [])

  function handleShowDeleteEmployeeModal(employee: Employee){
    showModal(<EmployeeDeleteModal employee={employee} onClose={hideModal} onDelete={handleDeleteEmployee}/>)
  }

  async function handleDeleteEmployee(employeeId: string) {
    const response = await deleteEmployee(employeeId)
    if(response.isSuccess) {
      const employeesAfterDelete = employees.filter(e => e.id != employeeId)
      setEmployees([...employeesAfterDelete])
    } 
  }

  return (
    <Box mt={2}>
      {modal}
      <Typography variant="h6">Список сотрудников</Typography>
      <Divider/>

      <Grid container mt={1}>
        {employees.map(e => <Grid key={e.id} item lg={3}>
            <Card>
              <CardContent sx={{position: 'relative'}}>
                <Stack spacing={2}>
                    <Stack flexDirection={"row"} gap={1}>
                      <AccountCircle/> 
                      <Typography variant="body1">{e.name}</Typography>
                    </Stack>
                    <Stack flexDirection={"row"} gap={1}>
                      <Phone/> 
                      <Typography variant="body1" gap={1}>{e.phone}</Typography>
                    </Stack>
                    <Stack flexDirection={"row"} gap={1}>
                      <Email/> 
                      <Typography variant="body1">{e.email}</Typography>
                    </Stack>
                </Stack>
                <Box sx={{position: 'absolute', right: 1, top: 1}}>
                  <IconButton size="small">
                    <Edit/>
                  </IconButton>
                  <IconButton size="small" onClick={() => handleShowDeleteEmployeeModal(e)}>
                    <Delete/>
                  </IconButton>
                </Box>
              </CardContent>
          </Card>
        </Grid>)}
      </Grid>
    </Box>
  )
}

async function deleteEmployee(employeeId: string): Promise<Result> {
  return await EmployeeProvider.delete(employeeId)
}

async function getEmployees(): Promise<Employee[]> {
  return await EmployeeProvider.list()
}

export default EmployeesList