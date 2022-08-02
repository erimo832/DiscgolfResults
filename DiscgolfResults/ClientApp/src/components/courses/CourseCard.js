import { CardContent, Card, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { useTranslation } from 'react-i18next';
import BarChartIcon from '@mui/icons-material/BarChart';
import { Col, Container, Row } from 'react-bootstrap';

export function CourseCard(props) {
    const { i18n } = useTranslation();

    return (
        <>            
            <Box component="span">
                <Card sx={{ minWidth: 275 }}>
                    <CardContent>
                        <Typography variant="h3" component="div">
                            <BarChartIcon /> {props.course.name}
                        </Typography>
                        <Container>
                            <Row>
                                <Col xs={12} md={6} lg={4}>
                                    <Container>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('course_card_numberofevents')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.course.numberOfEvents}</Col>
                                        </Row>
                                    </Container>
                                </Col>
                                <Col xs={12} md={6} lg={4}>
                                    <Container>
                                        <Row>
                                            <Col xs={6} md={6} lg={6}>{i18n.t('course_card_avgnumplayers')}</Col>
                                            <Col xs={6} md={6} lg={6} style={{textAlign: 'left'}}>{props.course.averageNumerOfPlayers}</Col>
                                        </Row>
                                    </Container>
                                </Col>
                            </Row>
                        </Container>
                    </CardContent>
                </Card>
            </Box>         
        </>
    ); 
}
