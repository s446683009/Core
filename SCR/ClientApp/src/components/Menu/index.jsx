import React, { useState } from 'react'
import PropTypes from 'prop-types'
import {TreeView,TreeItem } from '@material-ui/lab';
import {ChevronRight,ExpandMore as ExpandMoreIcon } from '@material-ui/icons';
import styled from './Menu.module.css';
function CustomMenu() {
    const [menuList,setMenu]=useState([]);



    return (
        <TreeView
        className={styled.treeView}
      defaultCollapseIcon={<ExpandMoreIcon />}
      defaultExpandIcon={<ChevronRight />}
    >
      <TreeItem className={styled.treeNode} nodeId="1" label="Applications">
        <TreeItem className={styled.treeNode} nodeId="2" label="Calendar" />
        <TreeItem className={styled.treeNode} nodeId="3" label="Chrome" />
        <TreeItem className={styled.treeNode} nodeId="4" label="Webstorm" />
      </TreeItem>
      <TreeItem className={styled.treeNode} nodeId="5" label="Documents">
        <TreeItem className={styled.treeNode} nodeId="10" label="OSS" />
        <TreeItem className={styled.treeNode} nodeId="6" label="Material-UI">
          
        </TreeItem>
      </TreeItem>
    </TreeView>
    )
} 

function transformMenu(data,parentId){


}


CustomMenu.propTypes = {

}






export default CustomMenu

