import React from 'react'
import CharacterCard from './CharacterCard'

class SwAPIHandler extends React.Component{
    constructor(){
        super()
        this.state = {
            data: [],
            name:"",
            loading:true,
            awaitingInput: true
        }
    }

    componentDidMount(){
        fetch("https://swapi.co/api/people/")
            .then(response => response.json())
            .then(data => {
                console.log(data.results)
                this.setState({
                    data:data.results,
                })
             console.log(this.state.data) 
            
        fetch('api/SwAPI/Characters', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(this.state.data)
        })
            .then(response => response.json())
            .then(data => this.setState({
                loading:false
            }))

    })}

    handleChange = (event) => {
        const {name, value} = event.target
        this.setState({
            [name]: value
        })
    }
    handleSubmit = () => {
        console.log(this.state.name)
        fetch('api/SwAPI/Characters/' + this.state.name, {
            method: 'GET'
        })
            .then(response => response.json())
            .then(data => this.setState({
                character : data,
                awaitingInput: false
            }))
            //.catch(error => console.log(error))  
    }

    render(){
        if(this.state.loading){
            return <h1>Loading...</h1>
        }
        let character = (this.state.awaitingInput)? <h1>Enter Data</h1>:<CharacterCard data = {this.state.character} />
        return (
                <div>
                    <div>
                        <input type="text" value={this.state.name} name="name" placeholder="Name" onChange={this.handleChange} />
                        <br />
                        <button onClick = {this.handleSubmit}> Submit </button>
                    </div>
                    {character}
                </div>    
        )
    }
}

export default SwAPIHandler