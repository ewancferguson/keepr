export class Account {
  /**
   * @param {{ id: string; email: string; name: string; picture: string; }} data
   */
  constructor(data) {
    this.id = data.id;
    this.email = data.email;
    this.name = data.name;
    this.picture = data.picture;
    // TODO add additional properties if needed
  }
}
