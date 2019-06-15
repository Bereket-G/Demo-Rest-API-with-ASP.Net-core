import axios from 'axios'

const client = axios.create({
    baseURL: 'http://localhost:5000/api/',
    json: true
});

export default {
        async execute (method, resource, data) {

            return client({
                method,
                url: resource,
                data
            }).then(req => {
                return req.data
            })
        },
        getTaskLists () {
            return this.execute('get', `/TaskList/`);
        },
        getTodoListsOfTask (taskId) {
            return this.execute('get', `/TaskList/` + taskId +`/todo` )
        },
        createTask (data) {
            return this.execute('post', '/Todo', data)
        },
        updateTask (id, data) {
            return this.execute('put', `/Todo/${id}`, data)
        },
        deleteTask (id) {
            return this.execute('delete', `/Todo/${id}`)
        }
}
