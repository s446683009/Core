import {Switch,HashRouter as Router} from 'react-router-dom'
import {  renderRoutes} from 'react-router-config';
import routes from './router-config'
import theme from './util/createTheme'
import { ThemeProvider } from '@material-ui/styles';



function App() {
    return (
        <ThemeProvider theme={theme}>
        <Router>
            <Switch>
                {renderRoutes(routes)}
            </Switch>
        </Router>  
        </ThemeProvider>
    )
    
}

export default App;
