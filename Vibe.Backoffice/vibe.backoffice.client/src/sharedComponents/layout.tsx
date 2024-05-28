import { Box, ButtonBase, IconButton, Stack, SxProps, Theme, Typography } from "@mui/material"
import { Settings, ExitToApp, ElectricScooter, Badge, SupportAgent } from '@mui/icons-material';
import { ReactNode, useState } from "react";
import { useAuthContext } from "../domain/infrastructure/authContext";
import { To, useNavigate } from "react-router-dom";
import React from "react";

interface IProps {
	children?: ReactNode
}

interface NavItemProps {
    title: string
    icon: ReactNode
    to: To
}

const tabs: NavItemProps[] = [
    {title: "Самокаты", icon: <ElectricScooter/>, to: '/scooters'},
    {title: "Сотрудники", icon: <Badge/>, to: '/employees'},
    {title: "Тех.поддержка", icon: <SupportAgent/>, to: '/supportRequests'}
]

function Layout(props: IProps) {
    const [currentTabIndex, setCurrentTabIndex] = useState<number | null>(null)
    const {logout} = useAuthContext()

    function handleClickTab(index: number){
        setCurrentTabIndex(index)
    }

    return (
        <Box sx={{ display: 'flex', width: '100%' }}>
            <Box sx={{borderRight: 1, borderRightStyle: 'dashed', borderRightColor: 'action.disabled'}} height={'100vh'}>
                <Stack spacing={2} mt={2} paddingX={1}>
                    <Typography align="center" variant='body1'>Vibe</Typography>
                    {tabs.map((tab, index) => 
                        <NavItem 
                            key={index}
                            title={tab.title} 
                            icon={tab.icon} 
                            to={tab.to}
                            isSelected={currentTabIndex === index}
                            onClick={() => handleClickTab(index)}
                        />
                    )}
                </Stack>
            </Box>
            <Box sx={{width: '100%'}} p={1}>
                <Stack flexDirection={"row"} justifyContent={'flex-end'}>
                    <IconButton>
                        <Settings/>
                    </IconButton>
                    <IconButton onClick={logout}>
                        <ExitToApp/>
                    </IconButton>
                </Stack>
                {props.children}
            </Box>
        </Box>
    )
}

function NavItem(props: NavItemProps & {isSelected: boolean, onClick: () => void}) {
    const navigate = useNavigate()

    const style: SxProps<Theme> = {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        borderRadius: 2,
        padding: 1,
        backgroundColor: props.isSelected 
            ? 'action.hover'
            : 'tranparent'
    }

    const colorStyle = {
        color: props.isSelected ? 'primary.main' : 'secondary',
    };

    function handleClick(){
        props.onClick()
        navigate(props.to)
    }

    return (
        <ButtonBase
            sx={style}
            onClick={handleClick}
        >
            {React.cloneElement(props.icon as React.ReactElement, {sx: colorStyle})}
            <Typography variant="body1" sx={colorStyle}>{props.title}</Typography>
        </ButtonBase>
    )
}

export default Layout