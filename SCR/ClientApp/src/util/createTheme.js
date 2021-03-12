
import {createMuiTheme} from '@material-ui/core/styles'
export default createMuiTheme({
    palette:{
        primary:{
            main:'#2f3c4e'
        },
        header:{
            main:'#fff'
        },
    },
    overrides: {
        // 样式表的名字 ⚛️
        MuiButton: {
          // 规则的名字
          root:{
              fontSize:"14px",
              background:'linear-gradient(45deg, #FE6B8B 30%, #FF8E53 90%)',
          },
          text: {
            // 一些 CSS
            color: 'white',
          },
        },
    }

})