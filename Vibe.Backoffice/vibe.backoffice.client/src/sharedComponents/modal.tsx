import { Box, Divider, Modal, Stack, Typography } from "@mui/material";
import { ReactElement } from "react";

interface CModalProps {
    title?: string,
    actions?: ReactElement
    children?: React.ReactNode
    onClose: () => void
}

function CModal(props: CModalProps) {

    const style = {
        position: 'absolute' as 'absolute',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
        width: 400,
        bgcolor: 'background.paper',
        boxShadow: 24,
        p: 4,
      };

    return (
        <Modal open onClose={props.onClose}>
            <Box sx={style}>
                <Typography variant="h6">{props.title}</Typography>
                <Divider/>

                {props.children &&
                    <Box mt={2}>
                        {props.children}
                    </Box>
                }

                {props.actions &&
                    <Stack flexDirection={"row"} gap={2} mt={2}>
                        {props.actions}
                    </Stack>
                }
            </Box>
        </Modal>
    )
}

export default CModal