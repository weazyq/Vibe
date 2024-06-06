export default class Error {
    constructor(
        public message: string,
        public key: string | null
    ) { }
}