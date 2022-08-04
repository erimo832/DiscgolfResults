import { CardContent, Card, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { useTranslation } from 'react-i18next';
import DonutLargeIcon from '@mui/icons-material/DonutLarge';
import { PieChart, Pie, Tooltip, Cell, ResponsiveContainer } from 'recharts';

export function HoleScoreDistributionPieChart(props) {
    const { i18n } = useTranslation();

    const eagle = "rgb(255, 139, 107)";
    const birdie = "rgb(76, 219, 183)";
    const par = "rgb(236, 237, 240)";
    const bogey = "rgb(255, 127, 177)";
    const double = "rgb(255, 89, 154)";
    const plus = "rgb(255, 51, 131)";
    const COLORS = [eagle, birdie, par, bogey, double, plus];
    
    function getContent(data) {
        return (
            <>
                <Box component="span">
                    <Card sx={{ minWidth: 275 }}>
                        <CardContent>
                            <Typography variant="h5" component="div">
                                <DonutLargeIcon /> {i18n.t('hole_stats_hole')} {data.holeNumber}
                            </Typography>
                            <ResponsiveContainer minWidth={150} minHeight={150}>
                                <PieChart width={350} height={350}>
                                    <Tooltip content={<HoleTooltip />} />
                                    <Pie data={data.scoreDistibutions} 
                                        dataKey="numberOfScores" 
                                        name='relativeScore'
                                        cx="50%" cy="50%" 
                                        outerRadius={60} fill="#8884d8">
                                        {data.scoreDistibutions.map((entry, index) => (
                                            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                                        ))}
                                    </Pie>
                                </PieChart>
                            </ResponsiveContainer>
                        </CardContent>
                    </Card>
                </Box>  
            </>
        )
    }
    
    return (
        <>
          {getContent(props.data)}
        </>
      );
}

const HoleTooltip = ({ active, payload, label }) => {
    const { i18n } = useTranslation();
  
    if (active && payload && payload.length) {
      let relative = i18n.t('hole_stats_relativescore_' + payload[0].payload.relativeScore);
      return (
        <div className="custom-tooltip">        
            {`${payload[0].payload.numberOfScores} ${relative}`}
        </div>
      );
    }
  
    return null;
  };