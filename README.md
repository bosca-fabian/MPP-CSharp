# MPP-CSharp

<b><i>MPP project that allows a user to register children to different sport trials.</i></b><br/><br/>

The database system used for the project is <b>PostgreSQL</b> because of the ease of use with pgAdmin4 and open sourceness. <br/><br/>
An ORM was also used for one of the entities, specifically <b>NHibernate</b> for the <i>Chlld</i> class, the mapping being done using and XML file 
with no other changes to the base class.<br/></br>

Locally, the app can run both a client and a server. The client's interface has been done using Windows Forms. Cross-platform wise, it has been tested and
can run a server to whom multiple Java clients have connected. The entirety of this feature was done using <b>Protocol Buffers</b> because of the wide use, the utility and open sourceness.
<br/><br/>
A REST client can also be found that has been used to test the working of a REST server written in Java for the entity <b><i>Trial</i></b>.
