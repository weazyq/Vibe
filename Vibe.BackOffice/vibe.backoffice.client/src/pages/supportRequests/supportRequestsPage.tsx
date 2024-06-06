import { Box, ButtonBase, Divider, Grid, Stack, Typography } from "@mui/material"
import { useEffect, useState } from "react"
import { SupportRequest } from "../../domain/techSupports/supportRequest"
import { SupportRequestProvider } from "../../domain/techSupports/supportRequestProvider"
import SupportRequestChat from "./supportRequestChat"

function SupportRequestsPage() {

  const [selectedSupportRequest, setSelectedSupportRequest] = useState<SupportRequest | null>(null)
  const [supportRequests, setSupportRequests] = useState<SupportRequest[]>([])

  useEffect(() => {
    loadSupportRequests()
  }, [])

  async function loadSupportRequests(){
    const supportRequests = await SupportRequestProvider.list()
    setSupportRequests(supportRequests)
  }

  console.log(selectedSupportRequest)

  return (
    <Box height={'87vh'}>
        <Typography variant="h6">Техническая поддержка</Typography>
        <Divider/>
        
        <Grid container height={'100%'} mt={1} spacing={1}>
          <Grid item xs={4}>
            <Stack spacing={0.5}>
              {supportRequests.map(request => 
                <ButtonBase
                  key={request.id}
                  sx={{
                    padding: 1,
                    justifyContent: 'flex-start',
                    textAlign: 'left',
                    border: 1,
                    borderRadius: 2,
                    borderColor: 'action.disabled',
                    position: 'relative'
                  }}

                  onClick={() => setSelectedSupportRequest(request)}
                >
                  <Stack>
                    <Typography variant="body1">{request.title}</Typography>
                    <Typography variant="body2">{request.description}</Typography>
                  </Stack>
                  <Box sx={{position: 'absolute', right: 5, top: 5}}>
                    {request.openedAt.toLocaleDateString()}
                  </Box>
                </ButtonBase>
            )}
            </Stack>
          </Grid>
          <Grid item xs={8}>
            <SupportRequestChat supportRequestId={selectedSupportRequest?.id ?? null}/>
          </Grid>
        </Grid>
    </Box>
  )
}

export default SupportRequestsPage