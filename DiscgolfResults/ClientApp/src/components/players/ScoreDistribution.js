import { useTranslation } from 'react-i18next';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

export function ScoreDistribution(props) {
    const { i18n } = useTranslation();

    return (
        <>            
            <ResponsiveContainer width="100%" height="100%" minHeight={300}>
                <BarChart width={500} height={300} data={props.data} margin={{ top: 5, right: 5, left: 5, bottom: 5 }} >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="score" />
                    <YAxis domain={[2, 'auto']} />
                    <Tooltip  />
                    <Legend />                    
                    <Bar name={i18n.t('player_details_distribution_numtimes')} dataKey="numberOfTimes" fill="#8884d8" />                    
                </BarChart>
            </ResponsiveContainer>       
        </>
    ); 
}