import React from 'react'

function CharacterCard(props){
    return (
        <div>
           <h1 style = {{color : (props.data.name.includes("Darth")) ? "red" : "blue"}}>{props.data.name}</h1> 
           <h3 style = {{color : "green"}}> -- {props.data.gender}</h3>
           <h3 style = {{color : "purple"}}> -- {props.data.height}</h3>
        </div>
        
    )
}
export default CharacterCard
